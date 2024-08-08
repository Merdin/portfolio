//
//  UIBarButtonItem+Extensions.swift
//  GoodList
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import UIKit

extension UIBarButtonItem {
    
    static func barButtonItemForAddTask(target: UIViewController, selector: Selector) -> UIBarButtonItem {
        let barButtonItem = UIBarButtonItem(barButtonSystemItem: .add, target: target, action: selector)
        return barButtonItem
    }
    
    static func barButtonItemForCancel(target: UIViewController, selector: Selector) -> UIBarButtonItem {
        let barButtonItem = UIBarButtonItem(barButtonSystemItem: .cancel, target: target, action: selector)
        return barButtonItem
    }
    
    static func barButtonItemForSave(target: UIViewController, selector: Selector) -> UIBarButtonItem {
        let barButtonItem = UIBarButtonItem(barButtonSystemItem: .save, target: target, action: selector)
        return barButtonItem
    }
}
