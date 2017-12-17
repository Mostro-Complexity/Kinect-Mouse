function xV=extended_kalman_filter(zV,q,r)
% function: simulating the process of EKF  
% q = 0.04;          % ���̱�׼��  
% r = 0.4;           % ������׼��  
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
xV = zeros(size(zV,1),1);  
 
for k = 1:size(zV,1)        
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
    x = x1+K*(zV(k,:)-z1);  
    % EKF�����Ӧline6  
    P = P-K*H*P;  
      
    % save  
    xV(k,:) = x;  
    % update process   
    s = f(s) + q*randn;  
end
end