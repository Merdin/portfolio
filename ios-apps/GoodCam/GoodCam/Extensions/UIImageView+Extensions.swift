//
//  UIImageView+Extensions.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import UIKit

extension UIImageView {
    
    static func imageForFilterView() -> UIImageView {
        let filterImageView = UIImageView(image: UIImage(named: "filter-default-image"))
        
        filterImageView.frame = CGRect(origin: CGPoint.zero, size: CGSize(width: 80, height: 80))
        filterImageView.layer.cornerRadius = 6.0
        filterImageView.layer.masksToBounds = true
        
        return filterImageView
    }
}
