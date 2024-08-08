//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct AppetizerListView: View {

	@StateObject var viewModel = AppetizerListViewModel()

	var body: some View {
		ZStack {
			NavigationView {
				List(viewModel.appetizers) { appetizer in
					AppetizerListCell(appetizer: appetizer)
						.onTapGesture {
							viewModel.selectedAppetizer = appetizer
							viewModel.isShowingDetail = true
						}
				}
				.navigationTitle("🍟 Appetizers")
				.listStyle(.plain)
				.disabled(viewModel.isShowingDetail)
			}
			.onAppear {
				viewModel.getAppetizers()
			}
			.blur(radius: viewModel.isShowingDetail ? 20 : 0)

			if viewModel.isShowingDetail {
				AppetizerDetailView(appetizer: viewModel.selectedAppetizer!, isShowingDetail: $viewModel.isShowingDetail)
			}

			if viewModel.isLoading {
				ProgressView()
			}
		}
		.alert(item: $viewModel.alertItem) { alert in
			Alert(
				title: alert.title,
				message: alert.message,
				dismissButton: alert.dismissButton
			)
		}
	}
}

struct AppetizerListView_Previews: PreviewProvider {
	static var previews: some View {
		AppetizerListView()
	}
}


