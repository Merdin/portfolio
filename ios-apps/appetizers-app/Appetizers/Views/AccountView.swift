//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI

struct AccountView: View {

	@StateObject private var viewModel = AccountViewModel()

    var body: some View {
		NavigationView {
			Form {
				Section("Personal info") {
					TextField("First Name", text: $viewModel.user.firstName)
					TextField("Last Name", text: $viewModel.user.lastName)
					TextField("Email", text: $viewModel.user.email)
						.keyboardType(.emailAddress)
						.textInputAutocapitalization(.never)
						.autocorrectionDisabled()
					DatePicker("Birthday", selection: $viewModel.user.date, displayedComponents: .date)
					Button {
						viewModel.saveChanges()
					} label: {
						Text("Save Changes")
					}
				}

				Section("Requests") {
					Toggle("Extra napkins", isOn: $viewModel.user.napkins)
					Toggle("Frequent Refills", isOn: $viewModel.user.refills)
				}
				.toggleStyle(SwitchToggleStyle(tint: Color("brandPrimary")))
			}
			.navigationTitle("Account")
		}
		.onAppear {
			viewModel.retrieveUser()
		}
		.alert(item: $viewModel.alertItem) { alert in
			Alert(title: alert.title, message: alert.message, dismissButton: alert.dismissButton)
		}
    }
}

struct AccountView_Previews: PreviewProvider {
    static var previews: some View {
        AccountView()
    }
}
