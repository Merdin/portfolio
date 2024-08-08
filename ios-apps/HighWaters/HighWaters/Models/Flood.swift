//
//  Flood.swift
//  HighWaters
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import Foundation
import Firebase

struct Flood {
    var id: String?
    let latitude: Double
    let longitude: Double
    let reportedDate: Date = Date()
}

extension Flood {
    init(latitude: Double, longitude: Double) {
        self.latitude = latitude
        self.longitude = longitude
    }
    
    init?(_ snapshot: QueryDocumentSnapshot) {
        guard let latitude = snapshot["latitude"] as? Double,
              let longitude = snapshot["longitude"] as? Double
        else {
            return nil
        }
        
        self.latitude = latitude
        self.longitude = longitude
        self.id = snapshot.documentID
    }
}

extension Flood {
    func toDictionary() -> [String: Any] {
        return [
            "latitude": self.latitude,
            "longitude": self.longitude,
            "reportedDate": self.reportedDate.formatAsString()
        ]
    }
}
