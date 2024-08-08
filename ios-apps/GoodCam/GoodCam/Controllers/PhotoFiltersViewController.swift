//
//  PhotoFiltersViewController.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation
import UIKit

protocol PhotoFilterViewControllerDelegate {
    func photoFilterCancel()
    func photoFilterDone()
}

class PhotoFiltersViewController: UIViewController, FiltersScrollViewDelegate {
    var image: UIImage?
    private var filtersService: FiltersService!
    
    var delegate: PhotoFilterViewControllerDelegate?
    
    @IBOutlet weak var filtersScrollView: FiltersScrollView!
    @IBOutlet weak var photoImageView: UIImageView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupUI()
    }
    
    private func setupUI() {
        self.filtersService = FiltersService()
        self.filtersScrollView.filterDelegate = self
        self.photoImageView.image = image
    }
    
    @IBAction func cancelButtonPressed() {
        self.delegate?.photoFilterCancel()
    }
    
    @IBAction func doneButtonPressed() {
        guard let selectedImage = self.photoImageView.image else {
            return
        }
        
        UIImageWriteToSavedPhotosAlbum(selectedImage, self, #selector(image(_:didFinishSavingWithError:contextInfo:)), nil)
    }
    
    @objc func image(_ image: UIImage, didFinishSavingWithError error: Error?, contextInfo: UnsafeRawPointer) {
        if let error = error {
            print(error.localizedDescription)
        } else {
            self.delegate?.photoFilterDone()
        }
    }
    
    func filterScorllViewDidSelectFilter(filter: CIFilter) {
        self.filtersService.applyFilter(filter: filter, to: self.image!) {
            self.photoImageView.image = $0
        }
    }
}
