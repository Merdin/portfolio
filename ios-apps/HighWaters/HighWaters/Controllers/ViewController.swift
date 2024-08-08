//
//  ViewController.swift
//  HighWaters
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import UIKit
import MapKit
import CoreLocation
import Firebase

class ViewController: UIViewController, CLLocationManagerDelegate, MKMapViewDelegate {
    
    @IBOutlet weak var mapView: MKMapView!
    @IBOutlet weak var floodButton: UIButton!
    
    private(set) var floods = [Flood]()
    
    private var documentRef: DocumentReference!
    
    private lazy var db: Firestore = {
        return Firestore.firestore()
    }()
    
    private lazy var locationManager: CLLocationManager = {
        let manager = CLLocationManager()
        manager.desiredAccuracy = kCLLocationAccuracyBest
        manager.distanceFilter = kCLDistanceFilterNone
        manager.requestWhenInUseAuthorization()
        
        return manager
    }()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.locationManager.startUpdatingLocation()
        self.mapView.showsUserLocation = true
        self.mapView.delegate = self
        
        setupUI()
        configureObservers()
    }
    
    private func updateAnnotations() {
        DispatchQueue.main.async {
            self.mapView.removeAnnotations((self.mapView.annotations))
            self.floods.forEach(self.addFloodToMap)
        }
    }
    
    private func configureObservers() {
        
        self.db
            .collection("flooded-regions")
            .addSnapshotListener { [weak self] snapshot, error in
                guard let snapshot = snapshot,
                      error == nil else {
                    print("Error fetching document")
                    return
                }
                
                snapshot.documentChanges.forEach { diff in
                    if diff.type == .added {
                        if let flood = Flood(diff.document) {
                            self?.floods.append(flood)
                            self?.updateAnnotations()
                        }
                    } else if diff.type == .removed {
                        if let flood = Flood(diff.document) {
                            if let floods = self?.floods {
                                self?.floods = floods.filter { $0.id != flood.id }
                                self?.updateAnnotations()
                            }
                        }
                    }
                }
            }
    }
    
    func mapView(_ mapView: MKMapView, didUpdate userLocation: MKUserLocation) {
        
        let region = MKCoordinateRegion(
            center: self.mapView.userLocation.coordinate,
            span: MKCoordinateSpan(latitudeDelta: 0.08, longitudeDelta: 0.08)
        )
        self.mapView.setRegion(region, animated: true)
    }
    
    @IBAction func floodButtonPressed() {
        saveFloodToFirebase()
    }
    
    private func addFloodToMap(_ flood: Flood) {
        let annotation = FloodAnnotation(flood)
        annotation.coordinate = CLLocationCoordinate2D(
            latitude: flood.latitude,
            longitude: flood.longitude
        )
        annotation.title = "Flooded"
        annotation.subtitle = flood.reportedDate.formatAsString()
        self.mapView.addAnnotation(annotation)
    }
    
    func mapView(_ mapView: MKMapView, annotationView view: MKAnnotationView, calloutAccessoryControlTapped control: UIControl) {
        if let floodAnnotation = view.annotation as? FloodAnnotation {
            let flood = floodAnnotation.flood
            
            self.db
                .collection("flooded-regions")
                .document(flood.id!)
                .delete() { error in
                    if let error = error {
                        print("Error when removing the document \(error)")
                    }
                }
        }
    }
    
    func mapView(_ mapView: MKMapView, viewFor annotation: any MKAnnotation) -> MKAnnotationView? {
        if annotation is MKUserLocation { return nil }
        
        var annotationView = mapView.dequeueReusableAnnotationView(withIdentifier: "FloodAnnotationView")
        
        if annotationView == nil {
            annotationView = MKAnnotationView(annotation: annotation, reuseIdentifier: "FloodAnnotationView")
            annotationView?.canShowCallout = true
            annotationView?.image = UIImage(named: "flood-annotation")
            annotationView?.rightCalloutAccessoryView = UIButton.buttonForRightAccessoryView()
        }
        
        return annotationView
    }
    
    private func saveFloodToFirebase() {
        guard let location = self.locationManager.location else {
            return
        }
        
        var flood = Flood(latitude: location.coordinate.latitude, longitude: location.coordinate.longitude)
        
        self.documentRef = self.db
            .collection("flooded-regions")
            .addDocument(data: flood.toDictionary()) { [weak self] error in
                if let error = error {
                    print(error)
                } else {
                    flood.id = self?.documentRef.documentID
                    self?.addFloodToMap(flood)
                }
            }
    }
    
    private func setupUI() {
        self.floodButton.layer.cornerRadius = 16.0
        self.floodButton.layer.masksToBounds = true
    }
}

