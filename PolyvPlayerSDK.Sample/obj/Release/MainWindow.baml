   M S B A M L   `   `   `  ���� J  Fffmeplay, Version=2017.8.7.28625, Culture=neutral, PublicKeyToken=null=#clr-namespace:PolyvPlayerSDK.SamplePolyvPlayerSDK.Sample  P LPolyvPlayerSDK, Version=2018.6.3.28621, Culture=neutral, PublicKeyToken=nullG4clr-namespace:PolyvPlayerSDK;assembly=PolyvPlayerSDKPolyvPlayerSDK &     PolyvPlayerSDK.Sample.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:PolyvPlayerSDK.Sample     Effme4clr-namespace:PolyvPlayerSDK;assembly=PolyvPlayerSDK     -     �Name$  window�� � Title$ PolyvPlayerSDKDemo��$��706.5��$	��720��$
��1200��$��1177.091�� � 	Resources 6   0PolyvPlayerSDK.Sample.TimeSpanToSecondsConverter   TimeSpanToSecondsConverter  OpenIcon  	CloseIcon  PlayIcon  StopIcon  	PauseIcon  NextIcon  BackIcon  VolumeUpIcon 	 VolumeDownIcon 
 MuteIcon  
UnmuteIcon��%�  &	        &	      &	 �     &	 �    &	 '    &	 X    &	 �    &	     &	     &		 C    &	
 �    &	 �      F� ����p�[?���� @���� z.�����k"@+� �<4����@ $� ���� �Dg�����+� � $�     ӯ�z     H��#��@ H� $� � H���z��z H� $�  H�����@ H���z $� S    �#��@���@ $� 	F� ����pL:4��!��� @� $� �B� ���  �� �<4���� @b�� ��B� ��� �<4���� @��5��� �i� $� ��)�L:4�iѥh��5�X( $� ѥh�� � $�     �1kz     H��4����@ H� $� � H�kz1kz H� $�  H��<4����@ H�kz $� S    �4����@<4����@ $� 	F� B����        Qsx� _B�  @ѷ��"� �sx� 2n�     H��    "� 	F� +����        ѱ��    ѱ�� H��     H�	F� O����Γ    �/}�    �/}� H��Γ H��        � �     � �  H��     H�	F� ^���p    �3���?Q��z� 0@�    ����    ���    ��� H�ѣ H�Q��z� 0@	F� h���p���3���?ђ�[����{:h�J@�/� �        ��{:h�J@    ��{:h�J@�/� ��{:h�J@ H��     H�	F� �����,�i��[ ������[ �����7� � H��7� � H�b3� ����b3� �������,�i���,�ib3� ��$b3� ��$�7� �,�i�7� �,��     �,�� �]w����?3@$��    $�Q    �_7��@yh���@�_7��@	F� u�����$氞 � H�氞 � H�b3� ��$b3� �,��     �,�� �]w����?3@$��    $�Q    �Z���@yh���@�Z���@	F� ����p��[��� `f@Q����e< ��#@Q������ `f@Q H�<4����"@�1t��B� � H��� ь��(�р���u� ѱ�[�I� -1,�� �b �� Q -1�e< ��"@� $�     � $� @x}� z `�    `�     z � z  z 	F� ����p��[��� `f@Q����e< ��#@Q������ `f@Q H�<4����"@�1t��B� � H��� ь��(�р���u� ѱ�[�I� -1,�� �b �� Q -1�e< ��"@� $�     � $� @x}� z `�    `�     z � z  z 	����  �� EndPoint$ 0.003,1.012&� �� 
StartPoint$ -0.002,-0.006&�.���  � Color$ 	LightBlue�� � Offset$ 0Q�� $ 
LightGreen��$ 1Q�.��� -   $��MainLayoutGrid��$��Stretch=�$��0,0,0,3q� � RowDefinitions �� $��* ��� $��90 �.+�� -   $��Player�� � ColumnDefinitions �� �� $	��240 �.+�!  PolyvPlayerSDK.MediaElement  -   $��Media��
���   �	   LoadedBehavior$
	 Play=�$��0��$��0��$��
10,10,5,10q��� -   $��
Properties��$��1��$��0��$	��Top=�
 �� Orientation$
 Vertical=�$	��680��$��10,10,0,-90q�  �� 
TargetType  Key �� Property �� Value 	 Value��%�  &	       &	 �     ']� Z    �� " ��.���  � ��$
 Left=���  � ��$
 Left=���  � ��$
 Bold"���  � ��$ 10(���  � ��
 �������  � ��$ 10,0,1,1q���  � ��$	 120���� " ��.���  � ��$ Right=���  � ��$ Stretch=���  � ��$ Normal"���  � ��$ 10(���  � ��
 �������  � ��$ 0,0,1,1q��� " ]�.���  � �� . .+�]� 
����ح�.+��� #��[ .��Media Format�� #��[ ����  �� ElementName$ Media�� �� Path$ MediaFormat�]� .+��� #��[ .��
Duration�� -   $��durationLbl��#��[ $��00:00:00��]� .+��� #��[ .��	Is Open�� #��[ ���� $ Media��$ IsOpen�]� .+��� #��[ .��
Is Opening�� #��[ ���� $ Media��$ 	IsOpening�]� .+��� #��[ .��Is Live Stream�� #��[ ���� $ Media��$ IsLiveStream�]� .+��� #��[ .��	Can Pause�� #��[ ���� $ Media��$ CanPause�]� .+��� #��[ .��Is Seekable�� #��[ ���� $ Media��$ 
IsSeekable�]� .+��� #��[ .��
Position�� #��[ ���� ���� $ Media��$ Position� �� StringFormat$ {0:hh\:mm\:ss}��]� .+��� #��[ .��Stay In Video�� -   $��stayInVideoLbl��#��[ $��00:00:00��]� .+��� #��[ .��Real Play Video�� -   $��realPlayVideoLbl��#��[ $��00:00:00��]� .+��� #��[ .��State�� #��[ ���� $ Media��$ 
MediaState�]� .+��� #��[ .��
Is Playing�� #��[ ���� $ Media��$ 	IsPlaying�]� .+��� #��[ .��	Has Ended�� #��[ ���� $ Media��$ HasMediaEnded�]� .+��� #��[ .��Is Buffering�� #��[ ���� $ Media��$ IsBuffering�]� .+��� #��[ .��Buffering Progress�� #��[ ���� $ Media��$ BufferingProgress�]� .+��� #��[ .��
Max Buffer�� #��[ ���� $ Media��$ BufferCacheLength�]� .+��� #��[ .��Download Progress�� #��[ ���� $ Media��$ DownloadProgress�]� .+��� #��[ .��Max Download�� #��[ ���� $ Media��$ DownloadCacheLength�]� 
����ح�.+��� #��[ .��	Has Video�� #��[ ���� $ Media��$ HasVideo�]� .+��� #��[ .��Video Codec�� #��[ ���� $ Media��$ 
VideoCodec�]� .+��� #��[ .��Video Accelerator�� #��[ ���� $ Media��$ VideoHardwareDecoder�]� .+��� #��[ .��Video Bitrate�� #��[ ���� $ Media��$ VideoBitrate�]� .+��� #��[ .��Video Width�� #��[ ���� $ Media��$ NaturalVideoWidth�]� .+��� #��[ .��Video Height�� #��[ ���� $ Media��$ NaturalVideoHeight�]� .+��� #��[ .��	Video FPS�� #��[ ���� $ Media��$ VideoFrameRate�]� 
����ح�.+��� #��[ .��	Has Audio�� #��[ ���� $ Media��$ HasAudio�]� .+��� #��[ .��Audio Codec�� #��[ ���� $ Media��$ 
AudioCodec�]� .+��� #��[ .��Audio Bitrate�� #��[ ���� $ Media��$ AudioBitrate�]� .+��� #��[ .��Audio Channels�� #��[ ���� $ Media��$ AudioChannels�]� .+��� #��[ .��Audio Sampling�� #��[ ���� $ Media��$ AudioSampleRate�]� .+��� #��[ .��Audio Bits/Sample�� #��[ ���� $ Media��$ AudioBitsPerSample�� -	   $��Controls��$��1��$��	0,0,244,0q� ��%   'F�       '�� H     �� " F�.���  � ��
 �   ���  �@��Stretch$ Uniform=��� " ��.���  � ��$ 28�� �� $��28 ��� �� .+�]� $��0����. $��0,0,10,0q�.+��� -
   $��Right=�$��	10,0,10,0q� � Cursor$
 Hand������ $ window�� �� Mode$ OneWay=�$ CloseCommand�.��F� $
 Hand������  �� Source# [ $
 Data��� -   $��Right=�$��	10,0,10,0q����� $ window��$ OneWay=�$ OpenCommand�.��F� $
 Hand������ # [ $
 Data��� .��VID:�� -   $��
VIDTextBox��$	��300��$��6,3,0,0q�$	��Top=����	#FF666666$
��Bold"�$��14(�.��This is some sample text�� -    ��Name$ comboBox��$��48��$��	10,0,10,0q�$
 Hand���� -    ��Name$ 	tipsLabel��$�� ��$	��134���� -   $��Right=�$��删除��$��32��$��	10,0,10,0q�$
 Hand����Z� �� -   $��Right=�$��暂停��$��32��$��	10,0,10,0q�$
 Hand����Z� �� -   $��Right=�$��下载��$��32��$��	10,0,10,0q�$
 Hand����Z� �� -    ��Name$ localCheckBox��$��72��$��Right=�$��Center=�$��本地播放��$��	10,0,10,0q�$
 Hand��]� ��. $��1�� ��%   'F�       �� " F�.���  � ��$ 10����  � ��$ 10����  � �� �#333��  �@��Stretch$ Uniform=�.+�F� $
��Auto��$��10,5.667,0,10q� �� Stretch$
 Fill=�$��13.668������ # [ $
 Data���  �� TickFrequency$	 0.1Q� �� TickPlacement$ TopLeft=�$	��250�� � Minimum$	 0.0Q� � Maximum$	 2.0Q� � SmallChange$	 0.1Q� � LargeChange$	 0.1Q� � Value �� $ Media��$ 
SpeedRatio��� $��Center=�$��20,0,5,0q� d� 	IsChecked �� *	IsMuted+$ Media��F� $
��Auto��$��0,2.833,20,5.833q�$
 Fill=�$��24������ # [ $
 Data�F� $��0,2.833,0,5.833q�$
 Fill=�$��24.001��$
��Auto������ # [	 $
 Data�F� $��Right=�$
��Auto��$��0,2.833,0,5.833q�$
 Fill=�$��21������ # [ $
 Data��� $
 0.05Q�$ TopLeft=�$	 0.0Q�$	 1.0Q�$
 0.05Q�$	 0.1Q�$��5,0,5,0q� �� $ Media��$ Volume�]� ��. $��2��.+��� -   $��playBtn��$��72��$
��play��$��	10,0,10,0q�$
 Hand������ $ window��$ OneWay=�$ OperateCommand��� {�.  ���� $ window��$ OneWay=�$ PlayCommand�.��F� ���� # [ $
 Data��� {�.  ���� $ window��$ OneWay=�$ PauseCommand�.��F� ���� # [ $
 Data��� -   $��	10,0,10,0q����� $ window��$ OneWay=�$ StopCommand�.��F� $
 Hand������ # [ $
 Data��� $ 1Q�$ TopLeft=�
���   �$��Center=�$ 0Q�$
 0.05Q�$ 0.050Q�$��0,4,0,3q� �� $ Media��$ NaturalDuration�  �� 	Converter#  [   �� $ Media��$ Position�#  [  