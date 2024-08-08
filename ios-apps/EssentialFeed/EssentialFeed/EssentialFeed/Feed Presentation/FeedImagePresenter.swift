//
//  FeedImagePresenter.swift
//  EssentialFeed
//
//  Created by Merdin Kahrimanovic on 21-08-2023.
//

import Foundation

public final class FeedImagePresenter {
	public static func map(_ image: FeedImage) -> FeedImageViewModel {
		FeedImageViewModel(
			description: image.description,
			location: image.location
		)
	}
}
