//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

final class ImageLoader: ObservableObject {
	@Published var image: Image? = nil

	func load(from url: String) {
		NetworkManager.shared.downloadImage(from: url) { uiImage in
			guard let uiImage = uiImage else { return }

			DispatchQueue.main.async {
				self.image = Image(uiImage: uiImage)
			}
		}
	}
}

private struct RemoteImage: View {

	var image: Image?

	var body: some View {
		image?.resizable() ?? Image("food-placeholder")
	}
}


struct AppetizerRemoteImage: View {

	@StateObject var imageLoader = ImageLoader()

	let urlString: String

	var body: some View {
		RemoteImage(image: imageLoader.image)
			.onAppear {
				imageLoader.load(from: urlString)
			}
	}
}
