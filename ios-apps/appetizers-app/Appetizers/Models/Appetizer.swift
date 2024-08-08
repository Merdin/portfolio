//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import Foundation

struct Appetizer: Decodable, Identifiable {
	var id: Int
	let name: String
	let description: String
	let price: Double
	let imageURL: String
	let calories: Int
	let protein: Int
	let carbs: Int
}

struct AppetizerRoot: Decodable {
	let request: [Appetizer]
}

struct MockData {
	static let sampleAppetizer = makeAppetizer()

	static func makeAppetizer(id: Int = 0001) -> Appetizer {
		return Appetizer(
			id: id,
			name: "Test Appetizer",
			description: "This is the description for my test appetizer.",
			price: 9.99,
			imageURL: "",
			calories: 99,
			protein: 99,
			carbs: 99
		)
	}

	static let appetizers = [makeAppetizer(id: 0001), makeAppetizer(id: 0002), makeAppetizer(id: 0003), makeAppetizer(id: 0004), makeAppetizer(id: 0005)]
}
