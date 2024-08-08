//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct ScannerView: UIViewControllerRepresentable {

	@StateObject var viewModel = BarcodeScannerViewModel()

	func makeUIViewController(context: Context) -> ScannerVC {
		ScannerVC(scannerDelegate: context.coordinator)
	}

	func updateUIViewController(_ uiViewController: ScannerVC, context: Context) {}

	func makeCoordinator() -> Coordinator {
		Coordinator(scannerView: self)
	}

	final class Coordinator: NSObject, ScannerVCDelegate {

		private let scannerView: ScannerView

		init(scannerView: ScannerView) {
			self.scannerView = scannerView
		}

		func didFind(barcode: String) {
			scannerView.viewModel.scannedCode = barcode
		}

		func didSurface(error: CameraError) {
			switch error {
				case .invalidDeviceInput:
					scannerView.viewModel.alertItem = AlertContext.invalidDeviceInput
				case .invalidScannedValue:
					scannerView.viewModel.alertItem = AlertContext.invalidScannedType
			}
		}
	}
}
