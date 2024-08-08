//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

final class Order: ObservableObject {

	@Published var appetizers: [Appetizer] = []

	var totalPrice: Double {
		appetizers.reduce(0) { $0 + $1.price }
	}

	func add(_ appetizer: Appetizer) {
		appetizers.append(appetizer)
	}

	func removeItem(at offsets: IndexSet) {
		appetizers.remove(atOffsets: offsets)
	}
}
