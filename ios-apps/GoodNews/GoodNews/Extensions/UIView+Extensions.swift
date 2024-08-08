//
//  UIView+Extensions.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 05/08/2024.
//

import Foundation
import UIKit

extension UIView {
    
    static func viewForTableViewHeader(subtitle: String) -> UIView {
        let screenRectangle = UIScreen.main.bounds
        
        let subTitleView = UIView(
            frame: CGRect(
                origin: CGPoint.zero,
                size: CGSize(width: screenRectangle.width, height: 44)
            )
        )
        
        let subtitleLabel = UILabel(
            frame: CGRect(
                origin: CGPoint.zero,
                size: CGSize(width: screenRectangle.width, height: 44)
            )
        )
        
        subtitleLabel.text = subtitle
        subtitleLabel.textAlignment = .center
        subtitleLabel.textColor = UIColor.gray
        
        subTitleView.addSubview(subtitleLabel)
        return subTitleView
    }
    
    static func viewForSectionHeader(title: String) -> UIView {
        let screenRectangle = UIScreen.main.bounds
        let headerView = UIView(
            frame: CGRect(x: 15, y: 0, width: screenRectangle.width, height: 60)
        )
        
        headerView.backgroundColor = UIColor.white
        
        let sectionHeaderLabel = UILabel(frame: headerView.frame)
        sectionHeaderLabel.text = title.uppercased()
        sectionHeaderLabel.textColor = UIColor.black
        sectionHeaderLabel.font = UIFont.fontForSectionHeaderTitle()
        
        headerView.addSubview(sectionHeaderLabel)
        return headerView
    }
}
