//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct AppetizerListCell: View {
	let appetizer: Appetizer

	var body: some View {
		HStack {
			AppetizerRemoteImage(urlString: appetizer.imageURL)
				.aspectRatio(contentMode: .fit)
				.frame(width: 120, height: 90)
				.cornerRadius(8)

			VStack(alignment: .leading, spacing: 5) {
				Text(appetizer.name)
					.font(.title2)
					.fontWeight(.semibold)
				Text("$\(appetizer.price, format: .number)")
					.fontWeight(.light)
			}
			.padding(.leading, 6)
		}
	}
}

struct AppetizerListCell_Previews: PreviewProvider {
    static var previews: some View {
		AppetizerListCell(appetizer: MockData.sampleAppetizer)
    }
}
