//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct FrameworkGridView: View {

	@StateObject var viewModel = FrameworkGridViewModel()

    var body: some View {
		NavigationStack {
			ScrollView {
				LazyVGrid(columns: viewModel.columns) {
					ForEach(MockData.frameworks, id: \.id) { framework in
						NavigationLink(value: framework) {
							FrameworkTitleView(framework: framework)
						}
					}
				}
			}
			.navigationTitle("🍎 Frameworks")
			.navigationDestination(for: Framework.self) { framework in
				FrameworkDetailView(framework: framework)
			}
		}
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        FrameworkGridView()
			.preferredColorScheme(.dark)
    }
}
