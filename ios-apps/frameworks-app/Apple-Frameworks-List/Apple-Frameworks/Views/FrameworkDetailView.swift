//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct FrameworkDetailView: View {
	let framework: Framework
	@Binding var isShowingDetailView: Bool
	@State private var isShowingSafariView = false

    var body: some View {
		VStack {
			FrameworkCellView(framework: framework)

			Text(framework.description)
				.font(.body)
				.padding()

			Spacer()

			Button {
				isShowingSafariView = true
			} label: {
				Label("Learn more", systemImage: "book.fill")
			}
			.buttonStyle(.borderedProminent)
			.controlSize(.large)
			.buttonBorderShape(.roundedRectangle(radius: 20))
			.tint(.red)
		}
		.sheet(isPresented: $isShowingSafariView) {
			SafariView(url: URL(string: framework.urlString)!)
		}
    }
}

struct FrameworkDetailView_Previews: PreviewProvider {
    static var previews: some View {
		FrameworkDetailView(
			framework: MockData.sampleFramework,
			isShowingDetailView: .constant(false)
		)
		.preferredColorScheme(.dark)
    }
}
