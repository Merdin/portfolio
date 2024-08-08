//
//  Date+Extensions.swift
//  HighWaters
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import Foundation

extension Date {
    func formatAsString() -> String {
        let dateFormatter = DateFormatter()
        dateFormatter.dateFormat = "yyyy-MM-dd HH:mm:ss"
        return dateFormatter.string(from: self)
    }
}
