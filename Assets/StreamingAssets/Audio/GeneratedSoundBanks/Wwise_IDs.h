/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_FLAREIGNITE = 1139110184U;
        static const AkUniqueID PLAY_MAINGAMEMUSIC = 3907496076U;
        static const AkUniqueID PLAY_MAINMENU = 3738780720U;
        static const AkUniqueID PLAY_RANDOMINSIDE = 4209940251U;
        static const AkUniqueID PLAY_RANDOMOUTSIDE = 896793584U;
        static const AkUniqueID PLAY_SONAR_PING_95840 = 1793578009U;
        static const AkUniqueID PLAYRANDOMTHINGIE = 4025941372U;
        static const AkUniqueID STOP_FLAREIGNITE = 2191334314U;
        static const AkUniqueID STOP_MAINGAMEMUSIC = 1520859230U;
        static const AkUniqueID STOP_MAINMENU = 890527358U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace BEINGATTACKED
        {
            static const AkUniqueID GROUP = 3239871325U;

            namespace STATE
            {
                static const AkUniqueID ATTACKED = 216219666U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SAFE = 938058686U;
            } // namespace STATE
        } // namespace BEINGATTACKED

        namespace INWATER
        {
            static const AkUniqueID GROUP = 440928865U;

            namespace STATE
            {
                static const AkUniqueID INBASE = 2519568939U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID OUTBASE = 1133774580U;
            } // namespace STATE
        } // namespace INWATER

    } // namespace STATES

    namespace SWITCHES
    {
        namespace MUSICSWITCH
        {
            static const AkUniqueID GROUP = 1445037870U;

            namespace SWITCH
            {
                static const AkUniqueID BEINGCHASED = 1753303624U;
                static const AkUniqueID INBASE = 2519568939U;
                static const AkUniqueID NEUTRAL = 670611050U;
            } // namespace SWITCH
        } // namespace MUSICSWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID CHASED = 493461511U;
        static const AkUniqueID DEPTH = 681025064U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID NEW_TRIGGER = 4163741908U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
