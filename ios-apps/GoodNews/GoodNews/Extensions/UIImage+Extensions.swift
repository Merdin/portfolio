//
//  UIImage+Extensions.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 05/08/2024.
//

import Foundation
import UIKit

extension UIImage {
    
    static func imageForPlaceHolder() -> UIImage {
        return UIImage(named: "placeholder")!
    }
    
    static func imageForHeadline(url: String, completion: @escaping (UIImage) -> ()) {
        guard let imageURL = URL(string: url) else {
            completion(UIImage.imageForPlaceHolder())
            return
        }
        
        DispatchQueue.global().async {
            if let data = try? Data(contentsOf: imageURL) {
                if let downloadImage = UIImage(data: data) {
                    completion(downloadImage)
                }
            }
        }
    }
}
