//
//  FeedLocalizationTests.swift
//  EssentialFeediOSTests
//
//  Created by Merdin Kahrimanovic on 19-08-2023.
//

import XCTest
import EssentialFeed

final class FeedLocalizationTests: XCTestCase {
	
	func test_localizedStrings_haveKeysAndValuesForAllSupportedLocalizations() {
		let table = "Feed"
		let bundle = Bundle(for: FeedPresenter.self)

		assertLocalizedKeyAndValuesExist(in: bundle, table)
	}
}
