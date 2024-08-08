//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct BarcodeScannerView: View {

	@StateObject var viewModel = BarcodeScannerViewModel()

    var body: some View {
		NavigationView {
			VStack {
				ScannerView()
					.frame(maxWidth: .infinity, maxHeight: 300)

				Spacer().frame(height: 60)

				Label("Scanned barcode:", systemImage: "barcode.viewfinder")
					.font(.title)

				Text(viewModel.statusText)
					.bold()
					.font(.largeTitle)
					.foregroundColor(viewModel.statusTextColor)
					.padding()
			}
			.navigationTitle("Barcode Scanner")
			.alert(item: $viewModel.alertItem) { alertItem in
				Alert(
					title: Text(alertItem.title),
					message: Text(alertItem.message),
					dismissButton: alertItem.dismissButton
				)
			}
		}
    }
}

struct BarcodeScannerView_Previews: PreviewProvider {
    static var previews: some View {
		BarcodeScannerView()
    }
}
