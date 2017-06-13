//
//  AdPopcornDAPlugin.m
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "AdPopcornDAPlugin.h"


UIViewController *UnityGetGLViewController();

static AdPopcornDAPlugin *_sharedInstance = nil; //To make AdBrixPlugin Singleton

@implementation AdPopcornDAPlugin

@synthesize bannerView = _bannerView;
@synthesize interstitialAd = _interstitialAd;
@synthesize popupAd = _popupAd;
@synthesize nativeAd = _nativeAd;
@synthesize oneSpotAd = _oneSpotAd;

@synthesize callbackHandlerName = _callbackHandlerName;



+ (void)initialize
{
	if (self == [AdPopcornDAPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (AdPopcornDAPlugin *)sharedAdPopcornDAPlugin;
{
	return _sharedInstance;
}

- (id)init
{
	self = [super init];
    
    if (self)
    {
        
    }
	return self;
}


- (void)setDABannerViewDelegate
{
    _bannerView.delegate = self;
}

- (void)setDAInterstitialAdDelegate
{
    _interstitialAd.delegate = self;
}

- (void)setDAPopupAdDelegate
{
    _popupAd.delegate = self;
}

- (void)setDANativeAdDelegate
{
    _nativeAd.delegate = self;
}

- (void)setDAOneSpotAdDelegate
{
    _oneSpotAd.delegate = self;
}

#pragma mark - DABannerViewDelegate
- (void)DABannerView:(DABannerView *)bannerView didFailToReceiveAdWithError:(DAError *)error
{
    NSLog(@"didFailToReceiveAdWithError bannerView : %@, error : %@", bannerView, error);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DABannerViewDidFailToReceiveAdWithError", [[error description] UTF8String]);
}

- (void)DABannerViewDidLoadAd:(DABannerView *)bannerView
{
    NSLog(@"DABannerViewDidLoadAd bannerView : %@", bannerView);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DABannerViewDidLoadAd", "");
}

- (void)DABannerViewWillLeaveApplication:(DABannerView *)bannerView
{
    NSLog(@"DABannerViewWillLeaveApplication bannerView : %@", bannerView);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DABannerViewWillLeaveApplication", "");
}

#pragma mark - DAInterstitialAdDelegate
- (void)DAInterstitialAd:(DAInterstitialAd *)interstitialAd didFailToReceiveAdWithError:(DAError *)error
{
    NSLog(@"didFailToReceiveAdWithError interstitialAd : %@, error : %@", interstitialAd, error);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DAInterstitialAdDidFailToReceiveAdWithError", [[error description] UTF8String]);
}

- (void)DAInterstitialAdDidLoad:(DAInterstitialAd *)interstitialAd
{
    NSLog(@"DAInterstitialAdDidLoad interstitialAd : %@", interstitialAd);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DAInterstitialAdDidLoad", "");
}

- (void)DAInterstitialAdWillLeaveApplication:(DAInterstitialAd *)interstitialAd
{
    NSLog(@"DAInterstitialAdWillLeaveApplication interstitialAd : %@", interstitialAd);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DAInterstitialAdWillLeaveApplication", "");
}

- (void)willOpenDAInterstitialAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAInterstitialAdWillOpen", "success");
}

- (void)didOpenDAInterstitialAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAInterstitialAdDidOpen", "success");
}

- (void)willCloseDAInterstitialAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAInterstitialAdWillClose", "success");
}

- (void)didCloseDAInterstitialAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAInterstitialAdDidClose", "success");
}

#pragma mark - DAPopupAdDelegate
- (void)DAPopupAd:(DAPopupAd *)popupAd didFailToReceiveAdWithError:(DAError *)error
{
    NSLog(@"didFailToReceiveAdWithError popupAd : %@, error : %@", popupAd, error);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DAPopupAdDidFailToReceiveAdWithError", [[error description] UTF8String]);
}

- (void)DAPopupAdDidLoad:(DAPopupAd *)popupAd
{
    NSLog(@"DAPopupAdDidLoad popupAd : %@", popupAd);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DAPopupAdDidLoad", "");
}

- (void)DAPopupAdWillLeaveApplication:(DAPopupAd *)popupAd
{
    NSLog(@"DAPopupAdWillLeaveApplication popupAd : %@", popupAd);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DAPopupAdWillLeaveApplication", "");
}

- (void)willOpenDAPopupAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAPopupAdWillOpen", "success");
}

- (void)didOpenDAPopupAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAPopupAdDidOpen", "success");
}

- (void)willCloseDAPopupAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAPopupAdWillClose", "success");
}

- (void)didCloseDAPopupAd
{
    UnitySendMessage([_callbackHandlerName UTF8String], "DAPopupAdDidClose", "success");
}

#pragma mark - DANativeAdDelegate
- (void)DANativeAdDidFinishLoading:(DANativeAd *)nativeAd
{
    NSLog(@"DANativeAdDidFinishLoading nativeAd : %@", nativeAd);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DANativeAdDidFinishLoading", [[nativeAd.nativeAdvertisingResultJson description] UTF8String]);
}

- (void)DANativeAd:(DANativeAd *)nativeAd didFailWithError:(DAError *)error
{
    NSLog(@"didFailWithError nativeAd : %@, error : %@", nativeAd, error);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DANativeAdDidFailWithError", [[error description] UTF8String]);
}

#pragma mark - DAOneSpotAdDelegate
- (void)DAOneSpotAd:(DAOneSpotAd *)onespotAd didFailToReceiveAdWithError:(DAError *)error
{
    NSLog(@"- (void)DAOneSpotAd:(DAOneSpotAd *)onespotAd didFailToReceiveAdWithError:(DAError *)error : %@, %@", onespotAd, error);
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdDidFailToReceiveAdWithError", [[error description] UTF8String]);
}

- (void)DAOneSpotAdDidLoad:(DAOneSpotAd *)onespotAd
{
    NSLog(@"- (void)DAOneSpotAdDidLoad:(DAOneSpotAd *)onespotAd");
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdDidLoad", "");
}

- (void)DAOneSpotAdWillLeaveApplication:(DAOneSpotAd *)onespotAd
{
    NSLog(@"- (void)DAOneSpotAdWillLeaveApplication:(DAOneSpotAd *)onespotAd : %@", onespotAd);
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdWillLeaveApplication", "");
}

- (void)willOpenDAOneSpotAd
{
    NSLog(@"- (void)willOpenDAOneSpotAd");
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdWillOpen", "success");
}

- (void)didOpenDAOneSpotAd
{
    NSLog(@"- (void)didOpenDAOneSpotAd");
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdDidOpen", "success");
}

- (void)willCloseDAOneSpotAd
{
    NSLog(@"- (void)willCloseDAOneSpotAd");
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdWillClose", "success");
}

- (void)didCloseDAOneSpotAd
{
    NSLog(@"- (void)didCloseDAOneSpotAd");
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdDidClose", "success");
}

- (void)didCompleteDAOneSpotVideoAd
{
    NSLog(@"- (void)didCompleteDAOneSpotVideoAd");
    UnitySendMessage([_callbackHandlerName UTF8String], "DAOneSpotAdDidCompleteVideoAd", "success");
}


// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	
    // DA
    void _AdPopcornDASetCallbackHandler(const char* handlerName)
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName]];
        NSLog(@"callbackHandlerName: %@", [[AdPopcornDAPlugin sharedAdPopcornDAPlugin] callbackHandlerName]);
    }
    
    void _DAInitWithBannerViewSize(DABannerViewSizeType size, float originX, float originY, const char* appKey, const char* spotKey)
    {
        DABannerView *bannerView = [[DABannerView alloc] initWithBannerViewSize:size origin:CGPointMake(originX, originY) appKey:[NSString stringWithUTF8String:appKey] spotKey:[NSString stringWithUTF8String:spotKey] viewController:UnityGetGLViewController()];
        
        [AdPopcornDAPlugin sharedAdPopcornDAPlugin].bannerView = bannerView;
    }
    
    
    void _DABannerViewSetAdRefreshRate(int adRefreshRate)
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].bannerView setAdRefreshRate:adRefreshRate];
    }
    
    void _DABannerViewSetDelegate()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin] setDABannerViewDelegate];
    }
    
    void _DAInterstitialAdInitWithKey(const char* appKey, const char* spotKey)
    {
        DAInterstitialAd *interstitialAd = [[DAInterstitialAd alloc] initWithKey:[NSString stringWithUTF8String:appKey] spotKey:[NSString stringWithUTF8String:spotKey] viewController:UnityGetGLViewController()];
        
        [AdPopcornDAPlugin sharedAdPopcornDAPlugin].interstitialAd = interstitialAd;
    }
    
    void _DAInterstitialAdSetDelegate()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin] setDAInterstitialAdDelegate];
    }
    
    void _DAInterstitialAdPresentFromViewController()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].interstitialAd presentFromViewController:UnityGetGLViewController()];
    }
    
    void _DAPopupAdInitWithKey(const char* appKey, const char* spotKey)
    {
        DAPopupAd *popupAd = [[DAPopupAd alloc] initWithKey:[NSString stringWithUTF8String:appKey] spotKey:[NSString stringWithUTF8String:spotKey]];
        
        [AdPopcornDAPlugin sharedAdPopcornDAPlugin].popupAd = popupAd;
        
    }
    
    void _DAPopupAdSetDelegate()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin] setDAPopupAdDelegate];
    }
    
    void _DAPopupAdPresentFromViewController()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].popupAd presentFromViewController:UnityGetGLViewController()];
    }
    
    void _DANativeAdInitWithKey(const char* appKey, const char* spotKey)
    {
        DANativeAd *nativeAd = [[DANativeAd alloc] initWithKey:[NSString stringWithUTF8String:appKey] spotKey:[NSString stringWithUTF8String:spotKey]];
        
        [AdPopcornDAPlugin sharedAdPopcornDAPlugin].nativeAd = nativeAd;
    }
    
    
    void _DANativeAdSetDelegate()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin] setDANativeAdDelegate];
    }
    
    void _DANativeAdLoadAd()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].nativeAd loadAd];
    }
    
    void _DANativeAdCallImpression(const char* impressionUrl)
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].nativeAd callImpression:[NSString stringWithUTF8String:impressionUrl]];
    }
    
    void _DANativeAdClick(const char* clickUrl)
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].nativeAd click:[NSString stringWithUTF8String:clickUrl]];
    }
    
    void _DACloseBannerView()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].bannerView removeFromSuperview];
        
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].bannerView stopAd];
    }
    
    void _DAOneSpotAdInitWithKey(const char* appKey, const char* spotKey)
    {
        DAOneSpotAd *oneSpotAd = [[DAOneSpotAd alloc] initWithKey:[NSString stringWithUTF8String:appKey] spotKey:[NSString stringWithUTF8String:spotKey]];
        
        [AdPopcornDAPlugin sharedAdPopcornDAPlugin].oneSpotAd = oneSpotAd;
        
    }
    
    void _DAOneSpotAdSetDelegate()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin] setDAOneSpotAdDelegate];
    }
    
    void _DAOneSpotAdPresentFromViewController()
    {
        [[AdPopcornDAPlugin sharedAdPopcornDAPlugin].oneSpotAd presentFromViewController:UnityGetGLViewController()];
    }
}

@end

