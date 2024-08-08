//
//  PhotoPreviewViewController.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation
import UIKit

class PhotoPreviewViewController: UIViewController {
    @IBOutlet weak var photoImageView: UIImageView!
    var previewImage: UIImage!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.photoImageView.image = self.previewImage
    }
}
