//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct OrderView: View {

	@EnvironmentObject var order: Order

	var body: some View {
		NavigationView {
			ZStack {
				VStack {
					List {
						ForEach(order.appetizers) { appetizer in
							AppetizerListCell(appetizer: appetizer)
						}
						.onDelete(perform: order.removeItem)
					}
					.listStyle(.plain)

					APButton(title: "\(order.totalPrice, format: .number) - Place Order")
						.padding(.bottom)
				}

				if order.appetizers.isEmpty {
					EmptyState(message: "You have no items in your order.\nPlease add an appetizer!")
				}
			}
			.navigationTitle("📝 Order")
		}
	}
}

struct OrderView_Previews: PreviewProvider {
	static var previews: some View {
		OrderView()
	}
}
