//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct XDismissButton: View {

	@Binding var isShowingDetailView: Bool

	var body: some View {
			Button {
				isShowingDetailView = false
			} label: {
				ZStack {
					Circle()
						.frame(width: 30, height: 30)
						.foregroundColor(.white)
						.opacity(0.6)

					Image(systemName: "xmark")
						.imageScale(.small)
						.frame(width: 44, height: 44)
						.foregroundColor(.black)
				}
			}
	}
}
