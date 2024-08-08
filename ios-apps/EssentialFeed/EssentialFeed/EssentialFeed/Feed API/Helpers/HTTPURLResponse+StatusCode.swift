//
//  HTTPURLResponse+StatusCode.swift
//  EssentialFeed
//
//  Created by Merdin Kahrimanovic on 21-08-2023.
//

import Foundation

extension HTTPURLResponse {
	private static var OK_200: Int { return 200 }

	var isOK: Bool {
		return statusCode == HTTPURLResponse.OK_200
	}
}
