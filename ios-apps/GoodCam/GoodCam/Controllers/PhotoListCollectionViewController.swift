//
//  PhotoListCollectionViewController.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation
import UIKit
import Photos

protocol PhotoListCollectionViewControllerDelegate {
    func photoListDidSelectImage(selectedImage: UIImage)
}

class PhotoListCollectionViewController: UICollectionViewController {
    
    private var images = [PHAsset]()
    var delegate: PhotoListCollectionViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        populatePhotos()
    }
    
    private func requestPermission(completion: @escaping (PHAuthorizationStatus) -> ()) {
        PHPhotoLibrary.requestAuthorization(for: .readWrite) { status in
            DispatchQueue.main.async {
                completion(status)
            }
        }
    }
    
    override func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return self.images.count
    }
    
    override func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        let asset = self.images[indexPath.row]
        let manager = PHImageManager.default()
        
        let options = PHImageRequestOptions()
        options.isSynchronous = true
        
        manager.requestImage(
            for: asset,
            targetSize: CGSize(width: 320, height: 480),
            contentMode: .aspectFill,
            options: options) { image, _ in
                if let image = image {
                    self.delegate?.photoListDidSelectImage(selectedImage: image)
                }
            }
    }
    
    override func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        guard let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "PhotoCollectionViewCell", for: indexPath) as? PhotoCollectionViewCell else {
            fatalError("PhotoCollectionViewCell was not found")
        }
        
        let asset = self.images[indexPath.row]
        let manager = PHImageManager.default()
        
        manager.requestImage(
            for: asset,
            targetSize: CGSize(width: 100, height: 100),
            contentMode: .aspectFill, options: nil) { image, _ in
                
                DispatchQueue.main.async {
                    cell.photoImageView.image = image
                }
            }
        
        return cell
    }
    
    private func populatePhotos() {
        requestPermission { [weak self] status in
            switch (status) {
            case .authorized:
                let assets = PHAsset.fetchAssets(with: .image, options: nil)
                
                assets.enumerateObjects { object, count, stop in
                    self?.images.append(object)
                }
                
                self?.images.reverse()
                self?.collectionView.reloadData()
                
                print(self?.images)
            default:
                print("Not authorized")
            }
        }
    }
}
