//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <AdPopcornOfferwall/AdPopcornOfferwall.h>
#import <AdPopcornOfferwall/AdPopcornStyle.h>



@interface AdPopcornOfferwallPlugin : NSObject <AdPopcornOfferwallDelegate>


@property (nonatomic, copy) NSString *callbackHandlerName;
@property (nonatomic, strong) NSMutableDictionary *userDataDictionaryForFilter;

+ (AdPopcornOfferwallPlugin *)sharedAdPopcornOfferwallPlugin;

- (void)setAdPopcornOfferwallDelegate;

- (void)setUserDataDictionaryForFilter:(NSString *)key value:(NSString *)value;



@end
