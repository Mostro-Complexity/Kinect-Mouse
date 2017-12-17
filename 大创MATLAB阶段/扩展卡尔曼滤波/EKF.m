% function: simulating the process of EKF  
q = 0.1;          % ���̱�׼��  
r = 0.2;           % ������׼��  
% eye����������λ����  
Q = q^2*eye(1);   % ���̷���  
R = r^2;               % ����ֵ�ķ���  
  
f = @(x)(x);  % ״̬����  
h = @(x)(x);                          % ��������  
s = 1;                                            % ��ʼ״̬  
  
% ��ʼ��״̬  
x = s+q*randn;   
% eye���ص�λ����  
P = eye(1);  
% ���Ź���ֵ  
xV = zeros(1,500);  

% ״̬����ֵ  
zV = s + r * randn(1,500);  
  
for k = 1:size(zV,2)  
  %  z = s + r * randn;  
    % ״̬����ֵ  ˵���˾�һ���ֵ
 %   zV(:,k) = z;  
      
    % ����f���ſɱȾ�������x1��Ӧ�ƽ�ʽline2  
    [x1,A] = jaccsd(f,x);  
    % ���̷���Ԥ�⣬��Ӧline3  
    P = A*P*A'+Q;  
    % ����h���ſɱȾ���  
    [z1,H] = jaccsd(h,x1);  
      
    % ���������棬��Ӧline4  
    % inv���������  
    K = P*H'*inv(H*P*H'+R);  
    % ״̬EKF����ֵ����Ӧline5  
    x = x1+K*(zV(:,k)-z1);  
    % EKF�����Ӧline6  
    P = P-K*H*P;  
      
    % save  
    xV(:,k) = x;  
    % update process   
    s = f(s) + q*randn;  
end

figure();
 

% �������Ź���ֵ  
plot(xV,'b-');  
hold on;  

% ����״̬����ֵ  
plot(zV,'r-');  
hold on;  

legend( 'EKF���Ź��ƹ���ֵ','״̬����ֵ');  
xlabel('ʱ��(����)');  
% ����ֵת�����ַ����� ת�������ʹ��fprintf��disp�������������  
t = ['״̬ ',num2str(k)] ;  
ylabel(t);  
