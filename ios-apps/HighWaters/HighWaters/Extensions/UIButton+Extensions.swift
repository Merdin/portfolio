//
//  UIButton+Extensions.swift
//  HighWaters
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import UIKit

extension UIButton {
    
    static func buttonForRightAccessoryView() -> UIButton {
        let button = UIButton(type: .custom)
        button.frame = CGRect(x: 0, y: 0, width: 18, height: 22)
        button.setImage(UIImage(named: "711-trash-toolbar"), for: .normal)
        return button
    }
}
