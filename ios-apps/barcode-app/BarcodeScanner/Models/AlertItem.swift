//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct AlertItem: Identifiable {
	let id = UUID()
	let title: String
	let message: String
	let dismissButton: Alert.Button
}


struct AlertContext {

	private static let alertButton: Alert.Button = .default(Text("OK"))

	static let invalidDeviceInput = AlertItem(
		title: "Invalid Device Input",
		message: "Something is wrong with the camera. We are unable to capture the input.",
		dismissButton: alertButton
	)

	static let invalidScannedType = AlertItem(
		title: "Invalid Scan Type",
		message: "The value scanned is not valid. This app scans EAN-8 and EAN-13.",
		dismissButton: alertButton
	)
}
