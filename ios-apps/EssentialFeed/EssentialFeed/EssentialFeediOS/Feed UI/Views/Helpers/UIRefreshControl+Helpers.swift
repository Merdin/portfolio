//
//  UIRefreshControl+Helpers.swift
//  EssentialFeediOS
//
//  Created by Merdin Kahrimanovic on 21-08-2023.
//

import UIKit

extension UIRefreshControl {
	func update(isRefreshing: Bool) {
		isRefreshing ? beginRefreshing() : endRefreshing()
	}
}
