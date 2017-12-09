%% A/D信号转化
% A2D is an analog to Digital converter
%
% Analog_value = analog value to digitise
% Bits = bit-resolution of A/D
% Volts = input volt-range of A/D
% time_length = time span of input signal
% Fs = sampling frequency in Hz
%
% Digital_value = Quantised digitised analog value
function [Digital_value] = a2d(Analog_value,Bits,Volts,Fs)
vpb=2*Volts/(2^Bits-1); % Volts per Bit
index=find(Analog_value>Volts);
if ~isempty(index)
Analog_value(index)=Volts;
end
clear index;
index=find(Analog_value<-Volts);
if ~isempty(index)
Analog_value(index)=-Volts;
end
Dig=round(Analog_value/vpb)*vpb; % Quantised Analog input
%% 如果要每个信号宽度相等，使用一下代码
% sample_step=floor(length(Dig)/Fs); % 跳变的最小间隔
% Digital_val=zeros(length(Dig),1);
% Digital_val(1:sample_step:end)=Dig(1:sample_step:end);
% for i=1:sample_step:length(Digital_val)
% Digital_val(i:i-1+sample_step)=Digital_val(i);
% end
% Digital_value=Digital_val(1:length(Dig)); % Digitised analog input
%% 如果不要，使用这个
Digital_value=Dig;
end