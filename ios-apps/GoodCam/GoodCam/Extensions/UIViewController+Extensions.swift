//
//  UIViewController+Extensions.swift
//  GoodCam
//
//  Created by Merdin Kahrimanovic on 08/08/2024.
//

import Foundation
import UIKit

extension UIViewController {
    func addChildController(_ vc: UIViewController) {
        self.addChild(vc)
        vc.view.frame = self.view.frame
        self.view.addSubview(vc.view)
        vc.didMove(toParent: self)
    }
}
