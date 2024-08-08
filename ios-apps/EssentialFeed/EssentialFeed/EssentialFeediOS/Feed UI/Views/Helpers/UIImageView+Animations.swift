//
//  UIImageView+Animations.swift
//  EssentialFeediOS
//
//  Created by Merdin Kahrimanovic on 19-08-2023.
//

import UIKit

extension UIImageView {
	func setImageAnimated(_ newImage: UIImage?) {
		image = newImage
		
		guard newImage != nil else { return }
		
		alpha = 0
		UIView.animate(withDuration: 0.25) {
			self.alpha = 1
		}
	}
}
