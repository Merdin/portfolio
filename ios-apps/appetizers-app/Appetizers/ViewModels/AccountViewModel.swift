//
//  Copyright © Merdin Kahrimanović. All rights reserved.
//  

import SwiftUI
import RegexBuilder

class AccountViewModel: ObservableObject {

	@AppStorage("user") private var userData: Data?
	@Published var user = User()

	@Published var alertItem: AlertItem?

	func saveChanges() {
		guard isValidForm else { return }

		do {
			let data = try JSONEncoder().encode(user)
			userData = data
			alertItem = AlertContext.userSaveSuccess
		} catch {
			alertItem = AlertContext.invalidUserData
		}
	}

	func retrieveUser() {
		guard let userData = userData else { return }

		do {
			user = try JSONDecoder().decode(User.self, from: userData)
		} catch {
			alertItem = AlertContext.invalidUserData
		}
	}

	var isValidForm: Bool {
		guard !user.firstName.isEmpty && !user.lastName.isEmpty && !user.email.isEmpty else {
			alertItem = AlertContext.invalidForm
			return false
		}

		guard user.email.isValidEmail else {
			alertItem = AlertContext.invalidEmail
			return false
		}

		return true
	}
}


private extension String {

	var isValidEmail: Bool {
		//        let emailFormat         = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}"
		//        let emailPredicate      = NSPredicate(format: "SELF MATCHES %@", emailFormat)
		//        return emailPredicate.evaluate(with: self)

		let emailRegex = Regex {
			OneOrMore {
				CharacterClass(
					.anyOf("._%+-"),
					("A"..."Z"),
					("0"..."9"),
					("a"..."z")
				)
			}
			"@"
			OneOrMore {
				CharacterClass(
					.anyOf("-"),
					("A"..."Z"),
					("a"..."z"),
					("0"..."9")
				)
			}
			"."
			Repeat(2...64) {
				CharacterClass(
					("A"..."Z"),
					("a"..."z")
				)
			}
		}

		return self.wholeMatch(of: emailRegex) !=  nil
	}
}




