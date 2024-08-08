//
//  FeedStore.swift
//  EssentialFeed
//
//  Created by Merdin Kahrimanovic on 05-08-2023.
//

import Foundation

public typealias CachedFeed = (feed: [LocalFeedImage], timestamp: Date)

public protocol FeedStore {
	typealias DeletionResult = Result<Void, Error>
    typealias DeletionCompletion = (DeletionResult) -> Void
	
	typealias InsertionResult = Result<Void, Error>
    typealias InsertionCompletion = (InsertionResult) -> Void
	
	typealias RetrievalResult = Result<CachedFeed?, Error>
	typealias RetrievalCompletion = (RetrievalResult) -> Void
	
	/// The completion handler can be invoked in any thread.
	/// Clients are responsible to dispatch to appropriate thread, if needed.
	func retrieve(completion: @escaping RetrievalCompletion)
	
	/// The completion handler can be invoked in any thread.
	/// Clients are responsible to dispatch to appropriate thread, if needed.
	func insert(_ feed: [LocalFeedImage], timestamp: Date, completion: @escaping InsertionCompletion)
    
	/// The completion handler can be invoked in any thread.
	/// Clients are responsible to dispatch to appropriate thread, if needed.
    func deleteCachedFeed(completion: @escaping DeletionCompletion)
}
