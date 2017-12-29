% ����BP�㷨��������

clear all;
close all;
clc;

% ���ɺͻ�������������������
P=[-1:0.05:1];
T=sin(2*pi*P)+0.1*randn(size(P));
plot(P,T,'k+');
hold on;
% ������ʵ����������
plot(P,sin(2*pi*P),':');
xlabel('x');
ylabel('f(x)');
title('Samples (+)and True Curve(---)');

% ����BP��������ò���(ͬѧ���Ե������溯���Ĳ���)
net=newff(P,T,[10,1],{'tansig','purelin'},'trainlm');
%net=newff(minmax(P),[15,1],{'tansig','purelin'},'traingdm');
%net=newff(minmax(P),[15,1],{'tansig','purelin'},'traingd');
net.trainParam.epoches=5000;
net.trainParam.goal=1.0e-3;

% ѵ������
[net,tr]=train(net,P,T);

% ����(�ع麯��ֵ��
A=sim(net,P);

% �������
e=T-A;
MSE=mse(e);

% �����ع���������ʵ����
figure(2);
plot(P,A,P,T,'k+',P,sin(2*pi*P),'k:');
xlabel('x');
ylabel('f(x)');
title('Samples(+),True Curve(dotted line) & Regress Curve(solid line)');
legend('����','������','��ʵ')


