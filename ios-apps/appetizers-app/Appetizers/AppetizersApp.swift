//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

@main
struct AppetizersApp: App {

	var order = Order()

    var body: some Scene {
        WindowGroup {
            AppetizerTabView()
				.environmentObject(order)
        }
    }
}
