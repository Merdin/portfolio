//
//  FeedImageViewModel.swift
//  EssentialFeed
//
//  Created by Merdin Kahrimanovic on 21-08-2023.
//

public struct FeedImageViewModel {
	public let description: String?
	public let location: String?
	
	public var hasLocation: Bool {
		return location != nil
	}
}
