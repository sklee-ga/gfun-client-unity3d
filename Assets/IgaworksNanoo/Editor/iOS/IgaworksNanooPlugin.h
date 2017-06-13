//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <IgaworksNanoo/IgaworksNanoo.h>



@interface IgaworksNanooPlugin : NSObject <IgaworksNanooDelegate>

@property (nonatomic, copy) NSString *callbackHandlerName;

+ (IgaworksNanooPlugin *)sharedIgaworksNanooPlugin;

- (void)setIgaworksNanooDelegate;


@end
