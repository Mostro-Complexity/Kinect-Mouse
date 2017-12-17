% Kalman filter example of temperature measurement in Matlab implementation of Kalman filter algorithm.  
% ���䵱ǰ�¶���ʵֵΪ24�ȣ���Ϊ��һʱ���뵱ǰʱ���¶���ͬ�����Ϊ0.02�ȣ�����Ϊ����������ʱ�����仯0.02�ȣ���  
% �¶ȼƵĲ������Ϊ0.5�ȡ�  
% ��ʼʱ�������¶ȵĹ���Ϊ23.5�ȣ����Ϊ1�ȡ�  
close all;  
%% ��������
% intial parameters  
% ��������n_iter��ʱ��  
n_iter = 100;  
% size of array. n_iter�У�1��  
sz = [n_iter, 1];  
% �¶ȵ���ʵֵ  
x = 24;  
% ���̷����Ӧ��������ʱ���¶ȷ�����Ĳ鿴Ч��  
Q = 4e-4;  
% ���������Ӧ�¶ȼƵĲ������ȡ����Ĳ鿴Ч��  
R = 0.25;  
% z���¶ȼƵĲ������������ʵֵ�Ļ����ϼ����˷���Ϊ0.25�ĸ�˹������  
z = x + sqrt(R)*randn(sz);  
% ��������г�ʼ��  
%% �˲���Ҫ����
% ���¶ȵĺ�����ơ�����kʱ�̣�����¶ȼƵ�ǰ����ֵ��k-1ʱ��������ƣ��õ������չ���ֵ  
xhat = zeros(sz);   
% ������Ƶķ���  
P = zeros(sz);   
% �¶ȵ�������ơ�����k-1ʱ�̣���kʱ���¶������Ĺ���  
xhatminus = zeros(sz);  % ����Ĳ�����������
% ������Ƶķ���  
Pminus = zeros(sz);  
% ���������棬��Ӧ���¶ȼƲ�����������ģ�ͣ�����ǰʱ������һʱ���¶���ͬ��һģ�ͣ��Ŀ��ų̶�  
K = zeros(sz);   
% intial guesses  
xhat(1) = 23.5; %�¶ȳ�ʼ����ֵΪ23.5��  
P(1) =1; % ����Ϊ1  
  
%% ��������
for k = 2:n_iter  
    % ʱ����£�Ԥ�⣩  
    % ����һʱ�̵����Ź���ֵ����Ϊ�Ե�ǰʱ�̵��¶ȵ�Ԥ��  
    xhatminus(k) = xhat(k-1);  
    % Ԥ��ķ���Ϊ��һʱ���¶����Ź���ֵ�ķ�������̷���֮��  
    Pminus(k) = P(k-1)+Q;  
    % �������£�У����  
    % ���㿨��������  
    K(k) = Pminus(k)/( Pminus(k)+R );  
    % ��ϵ�ǰʱ���¶ȼƵĲ���ֵ������һʱ�̵�Ԥ�����У�����õ�У��������Ź��ơ��ù��ƾ�����С������  
    xhat(k) = xhatminus(k)+K(k)*(z(k)-xhatminus(k));  
    % �������չ���ֵ�ķ���  
    P(k) = (1-K(k))*Pminus(k);  
end  
  

figure();  
plot(z,'k+'); %�����¶ȼƵĲ���ֵ  
hold on;  
plot(xhat,'b-','LineWidth',LineWidth) %�������Ź���ֵ  
hold on;  
plot(x*ones(sz),'g-','LineWidth',LineWidth); %������ʵֵ  
legend('�¶ȼƵĲ������', '�������', '��ʵֵ');  
xl = xlabel('ʱ��(����)');  
yl = ylabel('�¶�');  
set(xl,'fontsize',FontSize);  
set(yl,'fontsize',FontSize);  
hold off;  
set(gca,'FontSize',FontSize);  
  
figure();  
valid_iter = 2:n_iter; % Pminus not valid at step 1  
% �������Ź���ֵ�ķ���  
plot(valid_iter,P(valid_iter),'LineWidth',LineWidth);  
legend('������Ƶ�������');  
xl = xlabel('ʱ��(����)');  
yl = ylabel('��^2');  
set(xl,'fontsize',FontSize);  
set(yl,'fontsize',FontSize);  
set(gca,'FontSize',FontSize);  