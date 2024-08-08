//
//  UIButton+TestHelpers.swift
//  EssentialFeediOSTests
//
//  Created by Merdin Kahrimanovic on 14-08-2023.
//

import UIKit

extension UIButton {
	func simulateTap() {
		simulate(event: .touchUpInside)
	}
}
