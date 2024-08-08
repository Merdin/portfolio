//
//  AppContainerViewController.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation
import UIKit

class AppContainerViewController: UIViewController, PhotoListCollectionViewControllerDelegate, UINavigationControllerDelegate, UIImagePickerControllerDelegate, PhotoFilterViewControllerDelegate {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        guard let photoListCollectionViewController = self.children.first as? PhotoListCollectionViewController else {
            return
        }
        
        photoListCollectionViewController.delegate = self
    }
    
    @IBAction func cameraButtonPressed() {
        if UIImagePickerController.isSourceTypeAvailable(.camera) {
            let imagePickerViewController = UIImagePickerController()
            imagePickerViewController.sourceType = .camera
            imagePickerViewController.delegate = self
            self.present(imagePickerViewController, animated: true)
        }
    }
    
    func photoListDidSelectImage(selectedImage: UIImage) {
        showImagePreview(previewImage: selectedImage)
    }
    
    func imagePickerController(_ picker: UIImagePickerController, didFinishPickingMediaWithInfo info: [UIImagePickerController.InfoKey : Any]) {
        let originalImage = info[UIImagePickerController.InfoKey.originalImage] as! UIImage
        
        showPhotoFiltersViewController(for: originalImage)
        picker.dismiss(animated: true)
    }
    
    private func showPhotoFiltersViewController(for image: UIImage) {
        guard let photoFiltersViewController = self.storyboard?.instantiateViewController(identifier: "PhotoFiltersViewController") as? PhotoFiltersViewController else {
            fatalError("PhotoFiltersViewController not found")
        }
        
        photoFiltersViewController.image = image
        photoFiltersViewController.delegate = self
        self.addChildController(photoFiltersViewController)
    }
    
    func imagePickerControllerDidCancel(_ picker: UIImagePickerController) {
        picker.dismiss(animated: true)
    }
    
    private func showImagePreview(previewImage: UIImage) {
        guard let photoPreviewViewController = self.storyboard?.instantiateViewController(withIdentifier: "PhotoPreviewViewController") as? PhotoPreviewViewController else {
            fatalError("PhotoPreviewViewController not found")
        }
        
        photoPreviewViewController.previewImage = previewImage
        self.navigationController?.pushViewController(photoPreviewViewController, animated: true)
    }
    
    func photoFilterCancel() {
        showPhotosList()
    }
    
    func photoFilterDone() {
        showPhotosList()
    }
    
    private func showPhotosList() {
        self.view.subviews.forEach {
            $0.removeFromSuperview()
        }
        
        guard let photoListCollectionViewController = self.storyboard?.instantiateViewController(withIdentifier: "PhotoListCollectionViewController") as? PhotoListCollectionViewController else {
            fatalError("PhotoListCollectionViewController not found")
        }
        
        photoListCollectionViewController.delegate = self
        self.addChildController(photoListCollectionViewController)
    }
}

