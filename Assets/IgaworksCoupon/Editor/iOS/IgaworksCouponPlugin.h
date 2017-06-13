//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <IgaworksCoupon/IgaworksCoupon.h>



@interface IgaworksCouponPlugin : NSObject <IgaworksCouponDelegate>

@property (nonatomic, copy) NSString *callbackHandlerName;

+ (IgaworksCouponPlugin *)sharedIgaworksCouponPlugin;

- (void)setIgaworksCouponDelegate;


@end
