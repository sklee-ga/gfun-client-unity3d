//
//  AdPopcornDAPlugin.h
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <AdPopcornDA/DABannerView.h>
#import <AdPopcornDA/DAInterstitialAd.h>
#import <AdPopcornDA/DAPopupAd.h>
#import <AdPopcornDA/DANativeAd.h>
#import <AdPopcornDA/DAOneSpotAd.h>


@interface AdPopcornDAPlugin : NSObject <DABannerViewDelegate, DAInterstitialAdDelegate, DAPopupAdDelegate, DANativeAdDelegate, DAOneSpotAdDelegate>

@property (nonatomic, strong) DABannerView *bannerView;
@property (nonatomic, strong) DAInterstitialAd *interstitialAd;
@property (nonatomic, strong) DAPopupAd *popupAd;
@property (nonatomic, strong) DANativeAd *nativeAd;
@property (nonatomic, strong) DAOneSpotAd *oneSpotAd;

@property (nonatomic, copy) NSString *callbackHandlerName;

+ (AdPopcornDAPlugin *)sharedAdPopcornDAPlugin;

- (void)setDABannerViewDelegate;
- (void)setDAInterstitialAdDelegate;
- (void)setDAPopupAdDelegate;
- (void)setDANativeAdDelegate;
- (void)setDAOneSpotAdDelegate;

@end
