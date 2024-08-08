//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct FrameworkListView: View {

	@StateObject var viewModel = FrameworkGridViewModel()

	var body: some View {
		NavigationView {
			List {
				ForEach(MockData.frameworks, id: \.id) { framework in
					NavigationLink(
						destination: FrameworkDetailView(
							framework: framework,
							isShowingDetailView: $viewModel.isShowingDetailView
						)) {
							FrameworkCellView(framework: framework)
						}
				}
				.listRowBackground(Color.black)
			}
			.navigationTitle("🍎 Frameworks")
		}
		.accentColor(Color(.label))
	}
}

struct ContentView_Previews: PreviewProvider {
	static var previews: some View {
		FrameworkListView()
			.preferredColorScheme(.dark)
	}
}
