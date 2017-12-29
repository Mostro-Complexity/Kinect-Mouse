% 利用BP算法消除噪声

clear all;
close all;
clc;

% 生成和画出含噪声的正铉曲线
P=[-1:0.05:1];
T=sin(2*pi*P)+0.1*randn(size(P));
plot(P,T,'k+');
hold on;
% 画出真实的正弦曲线
plot(P,sin(2*pi*P),':');
xlabel('x');
ylabel('f(x)');
title('Samples (+)and True Curve(---)');

% 生成BP网络和设置参数(同学可以调节下面函数的参数)
net=newff(P,T,[10,1],{'tansig','purelin'},'trainlm');
%net=newff(minmax(P),[15,1],{'tansig','purelin'},'traingdm');
%net=newff(minmax(P),[15,1],{'tansig','purelin'},'traingd');
net.trainParam.epoches=5000;
net.trainParam.goal=1.0e-3;

% 训练网络
[net,tr]=train(net,P,T);

% 仿真(回归函数值）
A=sim(net,P);

% 计算误差
e=T-A;
MSE=mse(e);

% 画出回归曲线与真实曲线
figure(2);
plot(P,A,P,T,'k+',P,sin(2*pi*P),'k:');
xlabel('x');
ylabel('f(x)');
title('Samples(+),True Curve(dotted line) & Regress Curve(solid line)');
legend('仿真','加噪声','真实')


