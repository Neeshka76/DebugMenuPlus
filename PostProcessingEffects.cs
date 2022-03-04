using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DebugMenuPlus
{
    public class PostProcessingEffects : MonoBehaviour
    {
        private Volume volume;
        private ChannelMixer channelMixer;
        private float minChannelMixerClampValue;
        private float maxChannelMixerClampValue;
        public float redOutRedInValue;
        public float redOutGreenInValue;
        public float redOutBlueInValue;
        public float greenOutRedInValue;
        public float greenOutGreenInValue;
        public float greenOutBlueInValue;
        public float blueOutRedInValue;
        public float blueOutGreenInValue;
        public float blueOutBlueInValue;
        public bool changeValue;
        public bool overrideValue;

        private void Awake()
        {
            volume = this.gameObject.AddComponent<Volume>();
            volume.priority = 0;
            volume.isGlobal = true;
            volume.weight = 1f;
            volume.profile = new VolumeProfile();
            volume.profile.Add<ChannelMixer>();
            minChannelMixerClampValue = 0f;
            maxChannelMixerClampValue = 100f;
            changeValue = false;
        }
        private void Start()
        {
            volume.enabled = overrideValue;
            channelMixer.active = overrideValue;
            channelMixer.SetAllOverridesTo(overrideValue);
            if (volume.profile.TryGet(out channelMixer))
            {
                channelMixer.redOutRedIn = new ClampedFloatParameter(redOutRedInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.redOutGreenIn = new ClampedFloatParameter(redOutGreenInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.redOutBlueIn = new ClampedFloatParameter(redOutBlueInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.greenOutRedIn = new ClampedFloatParameter(greenOutRedInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.greenOutGreenIn = new ClampedFloatParameter(greenOutGreenInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.greenOutBlueIn = new ClampedFloatParameter(greenOutBlueInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.blueOutRedIn = new ClampedFloatParameter(blueOutRedInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.blueOutGreenIn = new ClampedFloatParameter(blueOutGreenInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
                channelMixer.blueOutBlueIn = new ClampedFloatParameter(blueOutBlueInValue, minChannelMixerClampValue, maxChannelMixerClampValue);
            }

        }

        private void Update()
        {
            if (changeValue)
            {
                if (volume.profile.TryGet(out channelMixer))
                {
                    volume.enabled = overrideValue;
                    channelMixer.active = overrideValue;
                    channelMixer.SetAllOverridesTo(overrideValue);
                    channelMixer.redOutRedIn.overrideState = overrideValue;
                    channelMixer.redOutGreenIn.overrideState = overrideValue;
                    channelMixer.redOutBlueIn.overrideState = overrideValue;
                    channelMixer.greenOutRedIn.overrideState = overrideValue;
                    channelMixer.greenOutGreenIn.overrideState = overrideValue;
                    channelMixer.greenOutBlueIn.overrideState = overrideValue;
                    channelMixer.blueOutRedIn.overrideState = overrideValue;
                    channelMixer.blueOutGreenIn.overrideState = overrideValue;
                    channelMixer.blueOutBlueIn.overrideState = overrideValue;
                    channelMixer.redOutRedIn.value = redOutRedInValue;
                    channelMixer.redOutGreenIn.value = redOutGreenInValue;
                    channelMixer.redOutBlueIn.value = redOutBlueInValue;
                    channelMixer.greenOutRedIn.value = greenOutRedInValue;
                    channelMixer.greenOutGreenIn.value = greenOutGreenInValue;
                    channelMixer.greenOutBlueIn.value = greenOutBlueInValue;
                    channelMixer.blueOutRedIn.value = blueOutRedInValue;
                    channelMixer.blueOutGreenIn.value = blueOutGreenInValue;
                    channelMixer.blueOutBlueIn.value = blueOutBlueInValue;
                }
                changeValue = false;
            }
        }
    }
}
