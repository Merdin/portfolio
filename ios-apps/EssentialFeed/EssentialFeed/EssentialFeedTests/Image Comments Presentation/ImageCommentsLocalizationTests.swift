//
//  ImageCommentsLocalizationTests.swift
//  EssentialFeedTests
//
//  Created by Merdin Kahrimanovic on 29-08-2023.
//

import XCTest
import EssentialFeed

class ImageCommentsLocalizationTests: XCTestCase {

	func test_localizedStrings_haveKeysAndValuesForAllSupportedLocalizations() {
		let table = "ImageComments"
		let bundle = Bundle(for: ImageCommentsPresenter.self)

		assertLocalizedKeyAndValuesExist(in: bundle, table)
	}
}
