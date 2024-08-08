//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

class AppetizerListViewModel: ObservableObject {

	@Published var appetizers: [Appetizer] = []
	@Published var alertItem: AlertItem?
	@Published var isLoading = false

	@Published var selectedAppetizer: Appetizer?
	@Published var isShowingDetail = false

	func getAppetizers() {
		isLoading = true

		NetworkManager.shared.getAppetizers { result in
			DispatchQueue.main.async { [self] in
				isLoading = false
				switch result {
					case let .success(appetizers):
						self.appetizers = appetizers
					case let .failure(error):
						switch error {
							case .invalidResponse:
								alertItem = AlertContext.invalidResponse
							case .invalidURL:
								alertItem = AlertContext.invalidURL
							case .invalidData:
								alertItem = AlertContext.invalidData
							case .unableToComplete:
								alertItem = AlertContext.unableToComplete
						}
				}
			}
		}
	}
}
