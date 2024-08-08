//
//  Date+Extensions.swift
//  GoodNews
//
//  Created by Merdin Kahrimanovic on 05/08/2024.
//

import Foundation

extension Date {
    static func dateAsStringForTableViewHeader() -> String {
        let formatter = DateFormatter()
        formatter.dateFormat = "MMM dd"
        return formatter.string(from: Date())
    }
}
