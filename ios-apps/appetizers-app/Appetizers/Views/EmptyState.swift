//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct EmptyState: View {

	let message: String

    var body: some View {
		ZStack {
			Color(.systemBackground)
				.ignoresSafeArea()

			VStack {
				Image("empty-order")
					.resizable()
					.scaledToFit()
					.frame(height: 150)

				Text(message)
					.font(.title3)
					.fontWeight(.semibold)
					.multilineTextAlignment(.center)
					.foregroundColor(.secondary)
					.padding()
			}
			.offset(y: -50)
		}
    }
}

struct EmptyState_Previews: PreviewProvider {
    static var previews: some View {
        EmptyState(message: "This is a test message.\n I'm making it longer for testing.")
    }
}
