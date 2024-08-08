//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct FrameworkDetailView: View {
	let framework: Framework
	@State private var isShowingSafariView = false

    var body: some View {
		VStack {
			FrameworkTitleView(framework: framework)

			Text(framework.description)
				.font(.body)
				.padding()

			Spacer()

			Button {
				isShowingSafariView = true
			} label: {
				AFButton(title: "Learn more")
			}
		}
		.sheet(isPresented: $isShowingSafariView) {
			SafariView(url: URL(string: framework.urlString)!)
		}
    }
}

struct FrameworkDetailView_Previews: PreviewProvider {
    static var previews: some View {
		FrameworkDetailView(framework: MockData.sampleFramework)
		.preferredColorScheme(.dark)
    }
}
