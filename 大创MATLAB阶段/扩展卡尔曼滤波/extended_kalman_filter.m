function xV=extended_kalman_filter(zV,q,r)
% function: simulating the process of EKF  
% q = 0.04;          % 过程标准差  
% r = 0.4;           % 测量标准差  
% eye函数产生单位矩阵  
Q = q^2*eye(1);   % 过程方差  
R = r^2;               % 测量值的方差  
  
f = @(x)(x);  % 状态方程  
h = @(x)(x);                          % 测量方程  
s = 1;                                            % 初始状态  
  
% 初始化状态  
x = s+q*randn;   
% eye返回单位矩阵  
P = eye(1);  
% 最优估计值  
xV = zeros(size(zV,1),1);  
 
for k = 1:size(zV,1)        
    % 计算f的雅可比矩阵，其中x1对应黄金公式line2  
    [x1,A] = jaccsd(f,x);  
    % 过程方差预测，对应line3  
    P = A*P*A'+Q;  
    % 计算h的雅可比矩阵  
    [z1,H] = jaccsd(h,x1);  
      
    % 卡尔曼增益，对应line4  
    % inv返回逆矩阵  
    K = P*H'*inv(H*P*H'+R);  
    % 状态EKF估计值，对应line5  
    x = x1+K*(zV(k,:)-z1);  
    % EKF方差，对应line6  
    P = P-K*H*P;  
      
    % save  
    xV(k,:) = x;  
    % update process   
    s = f(s) + q*randn;  
end
end