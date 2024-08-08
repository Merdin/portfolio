//
//  FloodAnnotations.swift
//  HighWaters
//
//  Created by Merdin Kahrimanovic on 04/08/2024.
//

import Foundation
import MapKit
import UIKit

class FloodAnnotation: MKPointAnnotation {
    
    let flood: Flood
    
    init(_ flood: Flood) {
        self.flood = flood
    }
    
    
}
