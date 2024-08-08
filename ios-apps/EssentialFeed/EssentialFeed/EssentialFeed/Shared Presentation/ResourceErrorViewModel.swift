//
//  ResourceErrorViewModel.swift
//  EssentialFeed
//
//  Created by Merdin Kahrimanovic on 21-08-2023.
//

public struct ResourceErrorViewModel {
	public let message: String?
	
	static var noError: ResourceErrorViewModel {
		return ResourceErrorViewModel(message: nil)
	}
	
	static func error(message: String) -> ResourceErrorViewModel {
		return ResourceErrorViewModel(message: message)
	}
}
